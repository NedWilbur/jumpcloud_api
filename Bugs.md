# Bugs
| Bug 1         |  |
|---|---|
| Title         | POST `/hash` does not return `jobId` immediately |
| Priority      | P2 |
| Severity      | Major |
| Environment   | Local |
| Build         | 0c3d817.1 |
| Prerequisites | Service running |
| Steps         | `curl -X POST -H "application/json" -d '{"password":"myPassword"}'` |
| Expected      | `jobId` is returned immediately |
| Actual        | `jobId` is returned after ~5 seconds |

| Bug 2         |  |
|---|---|
| Title         | POST `/hash` with empty password is successful |
| Priority      | P4 |
| Severity      | Minor |
| Environment   | Local |
| Build         | 0c3d817.1 |
| Prerequisites | Service running |
| Steps         | `curl -X POST -H "application/json" -d '{"password":""}'` |
| Expected      | `500`; No returned jobId |
| Actual        | `200` & jobId |
* Needs to be verified with PO/PM if actual bug or expected behavior. Not defined in acceptance criteria.

| Bug 3         |  |
|---|---|
| Title         | GET `/stats` returning invalid request total |
| Priority      | P4 |
| Severity      | Minor |
| Environment   | Local |
| Build         | 0c3d817.1 |
| Prerequisites | Service running |
| Steps         | `curl -X POST -H "application/json" -d '{"password":"myPassword"}'`</br>`curl http://127.0.0.1:8088/stats` |
| Expected      | `{"TotalRequests":1, … }` |
| Actual        | `{"TotalRequests":0, … }`</br>The count does not increase until the request has completed. |
* Needs to be verified with PO/PM if actual bug or expected behavior. Not defined in acceptance criteria.