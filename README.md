# SVG-from-Script
Editor and Tool for creating SVG and other image files from C# script. I created this tool to aid in the design of SVG for cutting items on a laser cutter. I have been using Excel to calculate the position of objects but wanted a better way to get right to the SVG file instead of copying and pasting locations into Inkscape.

This application uses C# scrip to generate SVG files and show a preview of the image. The preview may be saved in various image formats as well. The application works on the idea of creating and saving a project so changes can be made at a later time to tweak parameters and/or code.

Install the following NuGet packages:
-
- DockPanelSuite 3.4.0
- FCTB 2.16.24
- SvgNet 2.0.4
    - System.Drawing.Common 4.6.0
    - System.Drawing.Primitives 4.3.0
- Microsoft.CodeAnalysis.CSharp 3.4.0
    - Microsoft.CodeAnalysis.Analyzers 2.9.6
    - System.Buffers 4.4.0
    - System.Collections.Immutable 1.5.0
    - System.Numerics.Vectors 4.4.0
    - System.Reflection.Metadata 1.6.0
    - System.Runtime.CompilerServices.Unsafe 4.5.2
    - System.Memory 4.5.3
    - System.Text.Encoding.CodePages 4.5.1
    - System.Threading.Tasks.Extensions 4.5.3
    - Microsoft.CodeAnalysis.Common 3.4.0
-Microsoft.CodeAnalysis.CSharp.Scripting 3.4.0
	- Microsoft.CodeAnalysis.Common 3.4.0
	- Microsoft.CodeAnalysis.Scripting.Common 3.4.0
	- Microsoft.CSharp 4.7.0

