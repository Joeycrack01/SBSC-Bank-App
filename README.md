# SBSC-Bank-App

The SBSC bank app as developed by me is an asp.net core 3.1 api project well documented using swagger and also secure using jwt this means most of the endpoints requires an authorization token in the header which is used to Identify who is accessing these endpoints 

The SBSC bank app architectural design as seen below have are of types the monolithic architectural design and the microservice architectural design.

Although I have built the application as a monolithic but I also decided to add a microservice design for when it time to scale the application further.

Having scalability in mind affected some of the decisions I made during for instance, the database design is intentionally non relational because I figured that a company the size of SBSC will most likely have other products which employees might be required to use and that means at such point it won’t be in order for employees to have multiple credentials for these product, hence the need might arise to separate the user profile service so that it handles registration authentication for all products.

Should the need to separate some shared services arise, due to the non relational nature of the database design it will be easier as the system is already highly decoupled at that level.

The demilitarized zone is network space with very minimal security which houses the ui portal which is supposed to connect with the various backend service.

 Since the portals and website is to be opened for access to anyone on the internet it was necessary to include a gateway between the portals and the secured service so that the gateway securely re-routes users request to appropriate service without compromising security.
