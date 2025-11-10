# Search service

## Description
The goal of this service is to behave like a search engine for services. <br>
It should accept user inputs and return results that matched the provided service name.<br>


### Data

Example data:
```
[
    {
        "id": 1,
        "name": "Massage",
        "position": {
            "lat": 59.3166428,
            "lng": 18.0561182999999
        }
    },
    {
        "id": 2,
        "name": "Salongens massage",
        "position": {
            "lat": 59.3320299,
            "lng": 18.023149800000056
        }
    }
]
```

### Input
* Service name
* Geolocation


### Output

* TotalHits
* TotalDocuments
* Results

Results - an array of objects with properties
* Id
* Name
* Position
* Score
* Distance

The Results - list of services that matched the service name that was inputted. <br>
The Score - how awell the service name matched the search criteria. <br>
The Distance - how far away the result item is from the provided location.

Example output:
```
{
    "totalHits": 2,
    "totalDocuments": 10,
    "results": [
        {
            "id": 9,
            "name": "Massage",
            "position": {
                "lat": 59.40411099999999,
                "lng": 18.109118499999962
            },
            "distance": "8.95km",
            "score": 0
        },
        {
            "id": 3,
            "name": "Massör",
            "position": {
                "lat": 59.315887,
                "lng": 18.081163800000013
            },
            "distance": "100m",
            "score": 3
        }
    ]
}
```