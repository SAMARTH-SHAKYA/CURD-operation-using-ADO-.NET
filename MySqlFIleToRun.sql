create database StudentDB;
USE studentdb;
create table Students(
	ID int auto_increment primary key,
    Name varchar(100) not null,
    RollNum varchar(50) unique not null,
    Marks int
);

INSERT INTO Students (Name, RollNum, Marks)
VALUES ('Amit Sharma', 'R001', 85);

INSERT INTO Students (Name, RollNum, Marks)
VALUES ('Priya Verma', 'R002', 92);

INSERT INTO Students (Name, RollNum, Marks)
VALUES ('Rahul Mehta', 'R003', 76);

INSERT INTO Students (Name, RollNum, Marks)
VALUES ('Sneha Kapoor', 'R004', 88);

INSERT INTO Students (Name, RollNum, Marks)
VALUES ('Vikram Singh', 'R005', 95);

select * from students;