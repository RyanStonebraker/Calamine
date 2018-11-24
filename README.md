Calamine
========

# Introduction
Calamine is a graphical programming language with an Integrated Development Environment in virtual reality and a syntax based on the physical manipulation of virtual objects. Aimed at students with little to no programming experience, Calamine’s central goal is to teach the basics of programmatic logic (that is, breaking a complex problem into programmable concrete blocks) without encumbering the new programmers with a foreign syntax simultaneously. Calamine also introduces users to fundamental and universal programming constructs such as functions, flow-of-control statements, and user I/O, each intuitively represented using objects in the virtual environment.

# Licensing
All Calamine source code, object code, other software components, and all Audio/Visual assets are Copyright (C) 2016 Collin Lasley, Ryan Stonebraker, and Tristan Van Cise. All Rights Reserved.
**_CALAMINE IS NOT LICENSED AS FREE SOFTWARE_**

# Components
### Calamine Client
##### About
The client-side Calamine program is the software that would be deployed in schools, and includes software that both students and instructors would use. Despite the term "client" in this branch of the software, the Calamine Client is not solely a content consumption application; the Calamine Client is planned to include a lesson editor to allow instructors to create or refine their own Calamine Lessons.

##### Implementation
The Calamine Client is implemented using the Unity engine with Valve Software's SteamVR plugin to enable Virtual Reality support. Currently, lessons (interchangeably referred to as "levels") are loaded as Unity Scenes, although they will eventually be loaded from custom files using the _Calamine Plugin_ (see below). Events in Calamine Lessons are driven using Unity's C# scripting support.


### Calamine Plugin
The "Calamine Plugin" refers to a custom C++ Utility that Unity loads as a plugin when Calamine is started. The Calamine Plugin, while technically present, adds little functionality to the current version. When the lessons are later transitioned to the finalized format, the Plugin will handle interpretation of these files from Calamine's custom Netlist language to usable Unity objects.

### Calamine Development Environment (CDE)
##### About
The Calamine Development Environment is a suite of applications that facilitate low-level development of Calamine components and lessons. The CDE allows developers to implement new core functionality globally accessible throughout Calamine, choreograph lessons in great depth using an editor that supports features such as keyframing, triggers, and absolute positioning. Additionally, CDE provides utilities that allow rigging and animation of new 3D models, more advanced audio synchronization with these models, and subtitle editing.

##### Implementation
The CDE tools are very low-level, and do not directly interact with Unity at all. They are desktop applications (that is, not virtual reality) that run using a custom graphics framework written almost entirely in C. The output of the CDE tools are files that interact with the Calamine Plugin.

###### Where are these tools?
The CDE is not included in this repository. The CDE was written by Collin Lasley, who currently hosts the CDE and its source code on a developer-only repository on a separate private server.

# Usage
_The latest version of Unity that all components of Calamine, including the oldest demonstration files, has been tested to run on is 2017.3. It is very likely that newer versions will not break compatibility, but they may introduce warnings_

# Demonstration Levels

# Compatibility

## Repository Layout

# Developers
### Tristan Van Cise
About Tristan...

### Ryan Stonebraker
About Ryan...

### Collin Lasley
About me...

# Credits And Acknowledgements
For all those developers and other people who made our life easier while developing the alpha of Calamine!

