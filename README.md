
# WeatherMicroservice

The CQRS model also includes background services and event-based messaging tools, caching, etc. The general content of this project is a microservice project that provides weather information based on location.Weather information is taken from the OpenWeather website.

### Technologies and Libraries

- .NET 7
- ASP.NET Core API
- Postman
- Ef Core
- Swagger
- Visual Studio
- MediatR
- Serilog
- MongoDB
- PostgreSql
- MsSql
- Grpc
- RabbitMQ
- Serilog
- HealthCheck
- Redis
- Ocelot
- Mapper

### Architectural

 ![Sema](https://github.com/kadirdemirkaya/Weather-Microservice/assets/126807887/e94bbadb-21dc-49b5-bacc-16b037d00362)

 API Gateway: Ocelot and Nginx are tools that enable clients to connect to microservices.

Background Services: These services can operate without user interaction and generally ensure system-wide data integrity. In this section, it feeds the DataCapture service through events at certain time intervals.

Databases: Each microservice usually interacts with its own database. In this case, databases such as “Redis”, “PostgreSQL”, “MongoDB” and “MySQL” may be used. Each microservice provides isolation by using its own database.

Message Broker: RabbitMQ is a message broker and manages communication between microservices. When a microservice completes an operation, it can notify other microservices via RabbitMQ. This allows microservices to interact and interoperate with each other.

Docker + K8S: Docker allows each microservice to be run in a separate container. This allows each microservice to be deployed and scaled independently. K8S (Kubernetes) provides the management and coordination of these containers.

HTTP + gRPC: While HTTP provides text-based exchange of information between client and server, gRPC runs on HTTP/2 and exchanges data with low latency and high efficiency using Protobuf.

#### Services

- ApiGateway.Api : It does the job of redirecting external requests to my ClientAndServerService

- ClientAndServerService: It is a service that communicates with other services. It communicates with all services itself and the Client takes care of this.

- UserInfoService: It is a service that contains user transactions.

- DataCaptureService: Background services send API requests at certain time intervals and receive the responses (data) and then send them to the required service via events. In general, its purpose is to send requests and collect data within certain time intervals, thus keeping the data up-to-date.

- DataProcessService: It is the service that receives data through events and then processes it into the database. It can provide data quickly by establishing grpc communication to other services that request the data. It can store a certain number of information of the first requests made during caching.

- LocationService: Retrieves the weather information of the location requested by the user from the DataProcess service via grpc.
### Infrastructure

You need to enter the DataProcess and UserInfo services and apply migrations, then the ready seed data will be applied when the project is up and running.
You can run it via Terminal.

- dotnet ef migrations add 'MigrationName'
- dotnet ef database update

Apart from the technologies used in the project, server etc. If used, changes must be made to the .json extension files of the projects.

The data capture service must be entered in the background services and you must add an APIKEY with the key you obtained from the OpenWeather site.

If you want to start the project directly, you can apply the docker-compose file, the values ​​will be ready and you should see the services as in the picture.

- docker-compose up -d

![dck-dep](https://github.com/kadirdemirkaya/Weather-Microservice/assets/126807887/0867302e-60b3-465a-8c10-130feb8a6b80)

You can stand it up via kubernetes without needing any changes and you need to execute the command below. The services should look like the picture.

- kubectl apply -f '.yml PATH'

![kub-dep](https://github.com/kadirdemirkaya/Weather-Microservice/assets/126807887/6e7d9924-500e-463d-86a3-7f25517f9add)


