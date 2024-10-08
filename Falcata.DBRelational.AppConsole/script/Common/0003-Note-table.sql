create table main.notes (
    note_id int identity,
    note nvarchar(200) not null,
    
constraint PK_MAIN_NOTE primary key (note_id)
)