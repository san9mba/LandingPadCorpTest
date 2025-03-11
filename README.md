This is my general overview for this project. I didn't try to change a lot, only some fixes. Also as there are no any requirements for the logic I made it as simple as possible to show the main idead of implementation and moving this project to big unit.

This project must contain unit tests, but since writing unit tests takes a lot of time, I didn't add them. It's not because I'm lazy—it's because I didn't see the point. If you like my changes and code, I will explain what I will use for testing and how to test.

Made some refactors:
1. I didn't like that some business logic was in controllers, like checking if a user exists before updating. That should be handled in the service layer. The controller should only receive the request, validate it, handle authentication, and return a response.
2. In the GetUsers method, I moved the skip & take logic to the service layer because the service could return all records (if no filters are applied). The take parameter is only 1 or 10, meaning we were loading more records from the database than needed. Now, we fetch only the required data, optimizing both our code and database load.
3. I used a base method for try/catch to handle known issues like UserExists or EntityNotFound. I prefer returning data + a message from the service layer when these happen, avoiding exceptions, but for this project, it's fine.
Added validation to avoid exceptions and process only valid requests.
General notes:
There's a lot that could be improved, but that’s not the main focus of this task. Also, since this project is written in .NET Framework, some parts are outdated and, of course, could be improved.

Thanks for the review! Looking forward to discussing everything.

Let me know if you'd like any further tweaks!
