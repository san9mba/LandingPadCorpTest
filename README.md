Made some refactors:
1. I don't like that some buisness logics was at controllers, like check if user exists, before updating. That should be done at service layer. At controller we need to receive the request, validate request, some auth stuff and return response.
2. In GetUsers method, I moved skip&take logic to service layer, cause service could return all recors(if no filters) and take params is only 1 or 10, means we load more records from DB but we don't need it. So we take from DB as much data as we need and optimize our code and DB load.
3. Use base method for try/catch for known issues, like UserExsists or EntityNotFound. I prefer returning from Service layer data + message, if it happend and avoid throwing exception, but for this project it's fine.
4. add validation, to avoid exceptions and process only valid requests



As general, it's a lot of stuff that could be improved, but tasks is not about it and project wrote on .NET Framework, which means some stuff is old and ofcource could be improved.
Thanks for review and would be glad to discuss everything.
