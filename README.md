# Immersed-Challenge-Classroom
Unity 2020.3.28f1,
Universal Render Pipeline,
Normcore(networking), XR Interaction Toolkit, TextMeshPro

Features:
- Lobby Space – Choose Role, type name with a custom virtual Keyboard, and join the classroom.
- In the Classroom you will be spawned as a random avatar from the available option for your role, and within your area: Professor, will be spawned behind the desk, and students will be spawned randomly within a specified area in the scene.
- Once inside the space each role has certain abilities:

Everyone:
- Can teleport within the scene, - movement synced
- Can change the color of both desks, - colors synced
- Can grab the objects “objects” in the back of the class (cubes, and spheres) – transforms synced
- Can scale the objects with both hands’ “objects” in the back of the class (cubes, and spheres) – scale synced
- Can see the board and all the metadata of all the students (Id +name) + updates from each student (muted / hand raised), - board synced
- Can use the brush from their personal menu (brush strokes will be deleted when owner of these leaves the space, to keep it clean) – brush strokes synced

Student:
- Can Raise Hand from their personal menu and it will show up on the board – hand raise status synced

Professor:
- Can grab the objects “Special objects” in the back of the class (modeled blocks) – transforms synced
- Can scale the objects with both hands’ “Special objects” in the back of the class (modeled blocks)– scale synced
- Can Mute/ Unmute students by going to the custom mute keypad interface in their personal menu and typing the ID of the student and selecting to mute or unmute them. – mute and visibility status synced


Notes:
- Muted users are also invisible in the scene (set on an invisible layer to the camera), but they are present, can audit the class, use the brush, move and scale “objects” and raise their hands to be unmuted and set visible by the prof at any time.
- After the capacity of 7 avatars has been hit including the prof, any student that joins later will be muted by default.
- Prof can also mute themselves at any point to give more room for other students to be visible if necessary.
- For the professor to unmute people after 7, they will need to mute someone else, otherwise the unmute button will be greyed out. Once the visible users are less than 7, prof should close the menu and open it again to see the unmute button available. 
- If an object is grabbed, it will be considered owned by the person grabbing it, other users can not steal it (not to check every frame if the object is still grabbed). The user will try to pick it up, but the object will revert to the position / scale to the owner’s values.
- Changing desk colors will set it to the next color (4 colors available) 
- Brush is disabled from the menus in the desktop version and is only available in VR.


Applications:

VR application (included .apk /for oculus, etc.. and standalone .exe/ for other headsets with SteamVR)
- Primary button: Continuous press to show the personal menu (A and X on Oculus);
- Joystick Up and Down: to teleport (ray will appear)
- Trigger button: to interact with any object where a pointer and a ray are visible (UI, change desk colors)
- Trigger button: Continuous press to use the brush after brush is selected from the personal menu. Unselect the brush from the same button (now has a pressed appearance) to stop drawing.
- Grip: To grab object and scale objects with both hands

Desktop Application (Windows)
- SPACE: to open personal menu 
- AWSD: to move around
- Mouse Right Click:  To pan camera left and right
- Mouse Left click: To drag interactable objects and to change color of desks
- Hover over object + V: to scale up
- Hover over object + B: to scale down
