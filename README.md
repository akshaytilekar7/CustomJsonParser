# CustomJsonParser
Convert C# Object to Json

I have made one sample project which Convert any C# object to json.
There are already many plugin which provide the same functionality.
I have created this just for fun.


  
C# class :

  public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<string> lstHobbies { get; set; }
        }
        
Controller Code :-

          // my custom JsonParser class
          JsonParser jsonParser = new JsonParser();
          var Custom = jsonParser.ConvertToJsonString(employee);


 Output :-

        {"Id":1,"Name":"Akshay Tilekar","lstHobbies":["Coding","Problem Solving","UI Desigining"]}
 
