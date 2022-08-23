using System;
using ApiVox;
class Program {
    static void Main(string[] args)
    {
        string auth_string = "";
        string tts_test = "привет звук";
        ApiVox3i api=new ApiVox3i();
        var token=api.GetToken(auth_string);
        Console.WriteLine(api.Profile(token));
        //api.Tts("female", tts_test, token);
        


    }
    


}


