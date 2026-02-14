<div id="top"></div>
<!--
*** Thanks for checking out the Best-README-Template.
*** This README follows a consistent structure across projects
*** and is tailored to the specific technical and educational goals
*** of this VR Unity project.
-->

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-1F1F1F.svg?style=for-the-badge&logo=unity)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![HLSL](https://img.shields.io/badge/HLSL-%231A2B34.svg?style=for-the-badge&logo=shadertoy&logoColor=white)](https://docs.unity3d.com/Manual/SL-Reference.html)
[![Forks](https://img.shields.io/github/forks/your_username/repo_name.svg?style=for-the-badge)](https://github.com/your_username/repo_name/network/members)
[![Stars](https://img.shields.io/github/stars/your_username/repo_name.svg?style=for-the-badge)](https://github.com/your_username/repo_name/stargazers)
[![Issues](https://img.shields.io/github/issues/your_username/repo_name.svg?style=for-the-badge)](https://github.com/your_username/repo_name/issues)
[![MIT License](https://img.shields.io/github/license/your_username/repo_name.svg?style=for-the-badge)](LICENSE)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-black.svg?style=for-the-badge&logo=linkedin)](https://linkedin.com/in/your-profile)

<!-- PROJECT LOGO -->
<br />
<div align="center" style="background-color:rgba(0, 0, 0, 0.0470588)">
  <img src="Images/Logo/logo.png" alt="Logo" width="20%" height="20%">

  <h3 align="center">Sensor Sensation – VR Game for Immersive Media & Robotics</h3>

  <p align="center">
    A VR educational game exploring robotics sensor technologies through playful interaction.
    <br />
    <a href="#"><strong>Explore the project »</strong></a>
    <br />
    <br />
    <a href="https://youtube.com/your-demo-link">View Demo</a>
    ·
    <a href="https://github.com/your_username/repo_name/issues">Report Bug</a>
    ·
    <a href="https://github.com/your_username/repo_name/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

![Alt Game Play 1](Images/Intro/sensor-sensation-intro.gif)

**Sensor Sensation** is a **VR educational game developed in Unity for Oculus Rift S**, set in a stylized **steampunk-inspired world**. Players assume the role of **young robot engineers** who must solve interactive challenges by working with virtual sensor technologies such as **Camera, LiDAR and radar**.


The project’s primary goal is to help **decision-makers and non-technical stakeholders** develop an intuitive understanding of the **capabilities, limitations, and trade-offs of modern sensor systems** used in robotics and autonomous vehicles. Complex technical concepts are translated into playful, hands-on gameplay mechanics that encourage experimentation and learning through interaction.

The game was developed as a **student project by an interdisciplinary team**, combining technical development, visual design, and project management.

<p align="right">(<a href="#top">back to top</a>)</p>

### Game Play
![Alt Game Play 0](Images/Demo/demo0-sensor-sensation-intro.gif)

The game is set in a floating steampunk world in the clouds, experienced entirely through a VR headset. Players step into the role of a robotics engineer preparing for the annual Robo World Cup, a competition that tests both technical understanding and decision-making skills in sensor-based robotics.

Before accessing the main competition, players must complete a series of smaller preparatory challenges distributed throughout the world. These tasks reward points and gradually introduce core gameplay mechanics, encouraging exploration and learning through interaction.

![Alt Game Play 1](Images/Demo/demo1-sensor-sensation-intro.gif)

The VR game is structured into three main levels, each centered around a specific sensor technology. As players progress and unlock additional levels, they gain access to complementary sensors, mirroring real-world autonomous systems where LiDAR, radar, and cameras are combined to improve perception and robustness.

The final sensor, the camera, presents the environment as a pixelated image with bounding boxes, simulating computer-vision–based object detection. Players must interpret this limited visual information to identify and shoot down target objects, reinforcing the strengths and limitations of vision-based perception systems.

![Alt Game Play 2](Images/Demo/demo2-sensor-sensation-intro.gif)


### Demo Video

The following shows a full demo video:

[![Demo Video](https://img.youtube.com/vi/7oE6-0aCCRg/0.jpg)](https://www.youtube.com/watch?v=7oE6-0aCCRg)



### Built With

This project was built using the following technologies and tools:

- **Unity** – Game engine and VR framework
- **C#** – Gameplay logic, VR interaction systems, and game state management
- **HLSL** – Custom shaders for sensor visualization and stylized rendering
- **Oculus Rift S** – Target VR hardware platform
- **Blender** – Creation of custom 3D assets
- **Unity Particle System** – Visual feedback and sensor effects

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- <div align="center">
  <img src="Images/architecture.png" alt="Project Architecture" width="100%" height="100%">
</div> -->

<!-- GETTING STARTED -->

## Getting Started

This repository contains a **prototype VR game** developed as part of a **student project**.  
The focus was on **gameplay, interaction design, and sensor visualization**, rather than long-term maintainability or clean project architecture.

> ⚠️ The current project structure is exploratory and reflects fast-paced prototype development.

### Prerequisites

To open and run this project, you will need:

- **Unity Hub** with **Unity 2019.x – 2021.x LTS**  
  > The project was originally developed with an older Unity version. Newer versions may require minor fixes or package updates.
- **Oculus Rift S** headset
- Oculus desktop software installed and configured
- Windows PC with a **VR-capable GPU**
- Basic familiarity with Unity, C#, and VR projects

### Installation

1. **Clone the repository**
   ```sh
   git clone https://github.com/JaninaMattes/Robotics-Unity-VR-Game.git
