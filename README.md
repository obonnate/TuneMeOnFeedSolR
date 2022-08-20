# Program to fill a SolR index with sample content from Deezer

## Instructions

1. Deploy SolR cluster (ex: using a Helm Chart) and expose API endpoint out of the cluster (temporary, only for feeding purposes)
2. Register a developper account at Deezer, get an appid+appsecret
3. SolR requires a prebuilt schema (autogeneration is buggy as it forces all fields to be multivalued by default) :

  => Create a new 'catalogtrack' index collection from within SolR admin UI

  => Create the following field definitions inside the index (solR web admin UI again) :
  ````json
{
  "name": "album_image",
  "type": "string",
  "uninvertible": false,
  "indexed": true,
  "stored": true
}, {
  "name": "artist_name",
  "type": "text_en",
  "uninvertible": false,
  "indexed": true,
  "stored": true
}, {
  "name": "duration",
  "type": "pdouble",
  "uninvertible": false,
  "indexed": true,
  "stored": true
}, {
  "name": "genre",
  "type": "string",
  "uninvertible": true,
  "indexed": true,
  "stored": true
}, {
  "name": "id",
  "type": "string",
  "multiValued": false,
  "indexed": true,
  "required": true,
  "stored": true
}, {
  "name": "link",
  "type": "string",
  "uninvertible": false,
  "indexed": true,
  "stored": true
}, {
  "name": "name",
  "type": "text_en",
  "uninvertible": false,
  "indexed": true,
  "stored": true
}, {
  "name": "preview",
  "type": "string",
  "uninvertible": false,
  "indexed": true,
  "stored": true
}
  ````
  These definitions are included in the solution in the **\SolR** directory. "uninvertible" should be set to true only on fields requiring facetting, since a lot of unique values (names, ids, etc.) would waste a huge amount of memory.
  
4. Create 2 environment variables *SOLR_PASSWORD* and *DEEZER_APPSECRET* with your personal information and update the console application constants to reflect your APPID and solR endpoints.
5. Launch the application : it should connect to Deezer, retrieve some tracks and fill your index. Two sample requests are provided (standard and facetted).  

## Notes
1. SolrNet is not properly including Authorization headers in synchronous calls, hence the use of async everywhere in the program.
2. SolrNet can autocreate index fields but it will assume all fields are multivalued (ie mapped to collections and not scalars), thus it's required to create the schema before launching the application the first time.
3. In case the content of the index should be dropped, use the admin UI, go to collection/documents and issue an update query with a body
````xml
        <delete>
            <query>Emp_Work_Location:Deoria</query>
        </delete>
````
4. To use facets, the field must be marked as *uninvertible*
5. When creating a string field, use *string* only for ids used 'as is', for all searchable content use rather a localized version such as 'text_fr' otherwise content will be searchable as case-sensitive
