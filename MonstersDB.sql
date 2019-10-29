Create Table Monster (
	Id integer Identity primary key not null,
	Name varchar(50) not null,
	EyeCount integer not null,
	CatchPhrase varchar(255)
);


insert into Monster (name, eyecount, catchphrase) 
values ('Wolfman', 2, 'owwhouwhwu');

insert into Monster (name, eyecount, catchphrase) 
values ('Giant Spider', 8, 'what-chu-talkin-bout');

insert into Monster (name, eyecount, catchphrase) 
values ('cyclops', 1, 'I cannot see depth');


