Create Procedure InsertStudent
@FirstName nvarchar(50),
@LastName nvarchar(50),
@DateOfBirth nvarchar(50),
@Gender nvarchar(50),
@CourseId int,
@Result nvarchar(10) OUTPUT

As

Begin 
	Begin try
		Insert into [LearnDB].[dbo].[Students] values (@FirstName,@LastName,@DateOfBirth,@Gender,@CourseId);
		set @Result = 'Success';
	End try

	Begin catch
		Declare @ErrorMessage nvarchar(4000);
		Declare @ErrorSeverity int;
		Declare @ErrorState int;

		select 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();
		set @Result = 'Failure';

		RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState);
	end catch
end;
	
--------------------------------------------
SELECT student.FirstName , student.LastName,  course.CourseName,course.Instructor
                          FROM [LearnDB].[dbo].[Students] student
                          join [LearnDB].[dbo].[Courses] course on student.CourseId = course.CourseId;


select Student.FirstName , Student.LastName, Course.CourseName,
case 
	when Course.LecturesCount< 30 then 'Basic'
	when Course.LecturesCount <50 and Course.LecturesCount>30 then 'Intemediate'
	else 'Hard'
End as LectureCategory
from [LearnDB].[dbo].[Students] Student
join [LearnDB].[dbo].[Courses] Course on Student.CourseId= Course.CourseId


