create table main.users (
    user_id int identity,
    email nvarchar(50) not null,
    
    constraint PK_MAIN_USERS primary key (user_id)
)