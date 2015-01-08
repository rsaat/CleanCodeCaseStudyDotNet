CleanCodeCaseStudyDotNet
========================

This repository contains a [C#  version of CleanCodeCaseStudy](https://github.com/cleancoders/CleanCodeCaseStudy) , the source code of the video series Java Case Study from "Uncle Bob"  site [cleancoders.com/category/java-case-study](http://cleancoders.com/category/java-case-study). 

**The code presented here is not an "official" C# version of Clean Coders Java Case Study. It was created for personal usage but maybe someone that develops with .NET can find useful. The conversion from Java to C# was made using automated tools like  [Free Edition of Java to C# Converter from www.tangiblesoftwaresolutions.com](http://www.tangiblesoftwaresolutions.com/Demo.htm) and changed manually where the automated conversion did not work.**  

## Table of contents

- [Requirements](#requirements)
- [Install](#install)
- [Running Integrations Tests with Fitnesse](#running-integration-tests-with-fitnesse)
- [Running Unit Tests with NUnit](#running-unit-tests-with-nunit)
- [Episode Notes](#episode-notes)
     - [Episode 1](#episode-1) 
     - [Episode 2](#episode-2) 
     - [Episode 4](#episode-4) 
     - [Episode 5](#episode-5)

## Requirements

- Visual Studio 2013. The code uses .NET 4.0 and should be compatible  with Visual Studio 2010 or later versions. 
- Java runtime required by Fitnesse.
- [Fitnesse](http://www.fitnesse.org/)
- [Fitsharp](http://fitsharp.github.io/)
- [Nunit](http://www.nunit.org/) 

It´s required to install only the Visual Studio and Java runtime, the other free tools are already in the repository.

## Install
  
 1. Clone the repository to your local machine. 
 
 2. Each episode has a branch, for instance , Episode 1 has branch called Episode 1.X and a tag Episode 1.0 that should be equivalent of Episode1 tag from original Java version. If the code for the episode needs some corrections It will be created new tags like Episode1.1, Episode1.2,etc. **It was not ported all commits from the original java repository, only the tagged commits that correspond final code of each episode**.  
 
 3. Search for the Visual Studio solution file called **cleancoderscom.sln**. Initially the file was in the folder src\cleancoderscom and moved to the root folder at Episode 4. 

 4. Rebuild the solution and verify if there are no errors. 
  
##Running Integration Tests with Fitnesse

[Fitnesse](http://www.fitnesse.org/) is a web server and an automated testing tool for software that works with Java but supports other languages like C# using external runner called [Fitsharp](http://fitsharp.github.io/).

It is recommended that you learn the basics of Fitness, SLIM test engine mode, before using this code and viewing the videos.

####Some references to know FitNesse####

- [Wikipedia - FitNesse](http://en.wikipedia.org/wiki/FitNesse)
- [www.c-sharpcorner.com - Integration Testing With Fitnesse](http://www.c-sharpcorner.com/UploadFile/25c78a/integration-testing-with-fitnesse/)
- [Fitnesse](http://www.fitnesse.org/)

To run the integration tests

1. Rebuild the solution inside Visual Studio using debug mode. 

2. Start Fitnesse Web Server using the command line below where 8080 is the web server port.

    ```
    java -jar fitnesse-standalone.jar -p 8080
    ```
    
3. There is batch file with this command called **startfitnesse.bat**. 

4. Open a web browser using the address 

    ```
    http://localhost:8080/CleanCoders
    ```
    
5. Use the web interface to configure Fitness and run the tests. The folder where Fitnesse store the files is called FitNesseRoot\CleanCoders. 

6. You can launch the debugger to add break points in your code using the method 

    ```
    System.Diagnostics.Debugger.Launch();
    ```
    
    ```
    //Fixture Constructor
    //
    public CodecastPresentation()
    {
         System.Diagnostics.Debugger.Launch();   
        //...normal code  
    }
    ```
    
After Fitnesse is running you can verify some pages if you have some problem or just want to know where are defined the assembly and C# classes that Fitnesse will instantiate, see [the references above](#some-references-to-know-fitnesse) to understand how Fitnesse works.   

Assembly location and .NET fixture Fitsharp runner 

```
http://localhost:8080/CleanCoders
```

```
!contents

----
#Slim mode of Fitnesse 
!define TEST_SYSTEM {slim}

#Use .NET runner 
!define COMMAND_PATTERN {%m -r fitSharp.Slim.Service.Runner,Fitsharp\fitSharp.dll %p}
!define TEST_RUNNER {Fitsharp\Runner.exe}

#.NET assembly path where with C# classes
!path src\cleancoderscom\bin\Debug\cleancoderscom.dll
```

The C# namespace and C# class that Fitnesse will use

```
http://localhost:8080/CleanCoders.SetUp
```

```
|import|
|cleancoderscom.fixtures|
# cleancoderscom.fixtures corresponds to C# namespace

|library|
|codecast presentation|
# Fitnesse will intanciate a C# class called CodecastPresentation, case and spaces are ignored.
```

The Scenarios are similar to functions that you can use in Finesse tests 

```
http://localhost:8080/CleanCoders.EpisodeOnePresentCodeCasts.ScenarioLibrary
```

```
|scenario|and with license for _ able to view _|user,codecast|
|create license for |@user| viewing |@codecast|
# This scenario will call a C# function  called createLicenseForViewing(string user, string codecast) in the class CodecastPresentation defined with |library| option.  

```
 
##Running Unit Tests with NUnit

The Junit tests were converted to Nunit tests. Initially the tests and the project are in the same assembly and at Episode 4 the tests were moved to a different assembly.
There is a folder called NUnit-2.6.3 in the repository with the NUnit executables. It was tested with nunit-x86.exe gui test runner. 

## Episode Notes

### Episode 5
- Java Notify and Wait replaced with Monitor.Pulse and  Monitor.Wait see [StackOverflow - C# equivalent to java's wait and notify?](http://stackoverflow.com/questions/209281/c-sharp-equivalent-to-javas-wait-and-notify) and [CodeProject-Thread synchronization: Wait and Pulse demystified](http://www.codeproject.com/Articles/28785/Thread-synchronization-Wait-and-Pulse-demystified)
- Using .NET CountdownEvent to count the number of threads running and emulate Java executor.awaitTermination. 

### Episode 4
- Visual Studio solution moved to the root folder of repository.
- Test and Fixtures were moved to a separated assembly.
- There is no hierarchical context test runner for NUnit, but the tests works with nested classes like Java version of tests. Nested classes in Java and C# have some differences see [C# nested classes are like C++ nested classes, not Java inner classes](http://blogs.msdn.com/b/oldnewthing/archive/2006/08/01/685248.aspx). The tests are not totally independent requiring that the parent test class run before nested class tests. 
-  Java ServerSocket replaced with .NET TcpListener.
-  Java Socket replaced with .NET TcpClient
-  Java Executors.newFixedThreadPool(4) replaced with .NET ThreadPool.QueueUserWorkItem. 
-  It was not implemented the equivalent of Java executor.awaitTermination(500, TimeUnit.MILLISECONDS);

### Episode 2
- Added NUnit to Repository 

### Episode 1

- There are no unit tests.
- The C# Fitnesse fixtures and the application are in the same project. 
