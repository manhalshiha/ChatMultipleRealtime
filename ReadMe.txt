this project is using JWT Authentication & signalr for connect users together and make private chat with particulur user , 
also this project is using cersatile technic within c# like :
- user cascading parameter between mainlayout and chat.razor to pass value of AuthenticationState
- create hub and use websocket to connect users together
- generate token to secure every user , ( simply , without refresh)
- event handler and inheritance from INotifyPropertyChanged interface in AuthResponseDto.cs , and how i can invoke and method at change value for property in class which inheritance from INotifyPropertyChanged. using this technic allows for us to logout and navigate to login page  
==============
Required improvements:
======================
- simple database 
- simple design
- should use refresh token to authentication and add roles for authoraization
- should verify from user if he is logged from another browser 

=================
Highlights and main ideas:
==========================
02:38:48 add js file to save authentication in localstorage and use IJSRutime library
02:50:04 add JsonConverter class to use JsonSerilizer class and consume serilize methode to convert  object to string and versa vice with Desrilaize method.
03:03:06 create child component and use parameter to move data from parent to child
03:08:40 adding signalR library from nuget and setup HubConnection between hub on server and chat.razor page 
03:31:35 notify current user who are already connected users 
03:44:02 at click on any user , create new chat with him , using eventcallback to sent data from child to parent and trigger method within parent.
03:55:58 add logout functionality and use IJSRuntime
03:59:49 using INotifyPropertyChanged interface in AuthResponseDto.cs and event handler 
04:07:36 test and correct code for create special chat with any user i want from users list 
04:14:00 secure HubConnection Websocket with jwt authentication and authurization and test websocket working .
05:04:40 attach token with request through httpclient to get all user from api 
05:30:00 send all registered users to all users except current user , and here i would register new user and then send message to all connected users to display new user.
05:39:00 display online users status (red for who isn't online , green for who online )
05:50:00 when user have logged in , must to see other users if their online or not .
06:04:00 select user to begin chat with him
06:21:00 close conversation with any user then remove it from chatsList 
06:30:00 create form to write message and send it 
06:37:30 Send message to specific user 
07:27:00 creating history for every conversation 
07:53:00 when send message and sender is not exist in chats list then must adding to chats list 
08:02:00 creating new api to get users they have chat with current user (UserId) , it contains important query .

