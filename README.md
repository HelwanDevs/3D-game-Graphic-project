# 🏛️ FCAI Helwan University Building - 3D Project

This is the master repository for the University College Building 3D Visualization project. The project uses a Modular Linking Workflow to ensure smooth collaboration and high performance.

## Project Structure Explained

- `CS_Helwan.blend`  : Contains the Master Scene.
    - Note: This file is for assembly, lighting, and rendering ONLY. Do not model directly here.
- `Left_building/`  : Contains the source file for the left wing of the college and its sub-sections.
- `Right_building/` : Contains the source file for the right wing of the college and its sub-sections (like the basement).   
    - `Right_building/basement/` : Contains the source file for the right wing basement of the college.

## Description

- Each folder represents a section of the architectural project.
- Blender files (.blend, .blend1) are used for 3D modeling and backup.
    - Note : `.blend1 Files` These are automatic backups created by Blender. Do not delete them, but also do not work on them.

## Collaborative Workflow (Must Read)

To avoid file conflicts and ensure everything updates automatically, follow these rules:
1. The Power of Linking 🔗
We use File > Link (not Append) (no need to re-apply it again).

All building parts are linked into the Main_Building `CS_Helwan.blend` file as Collections.

If you edit Left_building.blend and save, the changes will reflect in the Master Scene (`CS_Helwan.blend`) automatically.

2. World Origin (0,0,0) Rule 📍Every team member must model their part relative to the World Origin ($0,0,0$).The 0,0,0 point in your file should match the anchor point in the Master Scene. This ensures that when we Link your file, it drops exactly in its correct position.


3. Usefull Add-ons 🔌
Auto Reload: can used in the Master Scene to see live updates from other team members without restarting Blender.

## How to start?

1. Open your assigned file (e.g., Left_building.blend).
2. Do your modeling/texturing.
3. Save (Ctrl + S).
4. The person responsible for the Master Scene will perform a Reload to see your work.

[!IMPORTANT]
DO NOT move, rename, or delete the folders. This will break the file paths (Links) and the Master Scene will show "Missing Data".