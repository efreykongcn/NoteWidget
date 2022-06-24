# OneNote Widget AddIn


A full featured Markdown viwer with syntax highlighting, export enhancement for OneNote

# Features

## Markdown Viewer
- Powered by [Markdig](https://github.com/lunet-io/markdig) - A powerful markdown parser, recommended by Microsoft
- CommonMark and GitHub flavored Markdown
- Syntax highlighting support
  - 100+ languages supported
- Dark/Light color scheme and code highlight theme support
  - Theme auto detection (following OS theme setting)
  - Multiple code highlight themes
- [Mermaid](https://github.com/mermaid-js/mermaid) chart support
  - Process, sequence, Gantt diagram etc. 
- Capturing picture in onenote page and display in Markdown viewer
- Preview Window Refresh (Press `F5`. 24/6/2022)
## Markdown CheatSheet
- Common mark
- Extended mark
- Diagram
  - Mermaid charts
## Export enhancement
- Export for Notebook/Section Group/Section/Page
- Single File and Hierarchical files options


## Demos 
**Syntax highlighting**  
```
    ```csharp
    namespace Demo
    {
        public class MyDemo
        {
            public static void Main(string[] args)
            {
                Console.WriteLine("Hello World!");
            }
        }
    }
    ```
``` 
*Markdown preview:*
```csharp
    namespace Demo
    {
        public class MyDemo
        {
            public static void Main(string[] args)
            {
                Console.WriteLine("Hello World!");
            }
        }
    }
```
**Mermaid pie chart**
```
    ```mermaid
    pie
      title Key elements in Product X
      "Calcium" : 40
      "Potassium" : 46
      "Magnesium" : 9
      "Iron" :  5

    ```
```  
*Markdown preview:*
<svg id="mermaid-1642657277801" width="100%" xmlns="http://www.w3.org/2000/svg" height="450" style="max-width: 1200px;" viewBox="0 0 1200 450"><style>#mermaid-1642657277801 {font-family:"trebuchet ms",verdana,arial,sans-serif;font-size:16px;fill:#ccc;}#mermaid-1642657277801 .error-icon{fill:#a44141;}#mermaid-1642657277801 .error-text{fill:#ddd;stroke:#ddd;}#mermaid-1642657277801 .edge-thickness-normal{stroke-width:2px;}#mermaid-1642657277801 .edge-thickness-thick{stroke-width:3.5px;}#mermaid-1642657277801 .edge-pattern-solid{stroke-dasharray:0;}#mermaid-1642657277801 .edge-pattern-dashed{stroke-dasharray:3;}#mermaid-1642657277801 .edge-pattern-dotted{stroke-dasharray:2;}#mermaid-1642657277801 .marker{fill:lightgrey;stroke:lightgrey;}#mermaid-1642657277801 .marker.cross{stroke:lightgrey;}#mermaid-1642657277801 svg{font-family:"trebuchet ms",verdana,arial,sans-serif;font-size:16px;}#mermaid-1642657277801 .pieCircle{stroke:black;stroke-width:2px;opacity:0.7;}#mermaid-1642657277801 .pieTitleText{text-anchor:middle;font-size:25px;fill:hsl(28.5714285714, 17.3553719008%, 86.2745098039%);font-family:"trebuchet ms",verdana,arial,sans-serif;}#mermaid-1642657277801 .slice{font-family:"trebuchet ms",verdana,arial,sans-serif;fill:#ccc;font-size:17px;}#mermaid-1642657277801 .legend text{fill:hsl(28.5714285714, 17.3553719008%, 86.2745098039%);font-family:"trebuchet ms",verdana,arial,sans-serif;font-size:17px;}#mermaid-1642657277801 :root{--mermaid-font-family:"trebuchet ms",verdana,arial,sans-serif;}</style><g></g><g transform="translate(600,225)"><path d="M46.00762912549813,179.18788480879675A185,185,0,0,1,-142.54494991352107,-117.92343810350752L0,0Z" fill="#0b0000" class="pieCircle"></path><path d="M1.1327982892113017e-14,-185A185,185,0,0,1,46.00762912549813,179.18788480879675L0,0Z" fill="#4d1037" class="pieCircle"></path><path d="M-142.54494991352107,-117.92343810350752A185,185,0,0,1,-57.1681439593653,-175.9454555146034L0,0Z" fill="#3f5258" class="pieCircle"></path><path d="M-57.1681439593653,-175.9454555146034A185,185,0,0,1,-3.398394867633905e-14,-185L0,0Z" fill="#4f2f1b" class="pieCircle"></path><text transform="translate(-78.10033310893637,49.56397853555722)" class="slice" style="text-anchor: middle;">40%</text><text transform="translate(91.7706098715892,-11.593324104698139)" class="slice" style="text-anchor: middle;">46%</text><text transform="translate(-51.99271245132211,-76.50495312039695)" class="slice" style="text-anchor: middle;">9%</text><text transform="translate(-14.470188016221371,-91.36117150505024)" class="slice" style="text-anchor: middle;">5%</text><text x="0" y="-200" class="pieTitleText">Key&nbsp;elements&nbsp;in&nbsp;Product&nbsp;X</text><g class="legend" transform="translate(216,-44)"><rect width="18" height="18" style="fill: rgb(11, 0, 0); stroke: rgb(11, 0, 0);"></rect><text x="22" y="14">Calcium</text></g><g class="legend" transform="translate(216,-22)"><rect width="18" height="18" style="fill: rgb(77, 16, 55); stroke: rgb(77, 16, 55);"></rect><text x="22" y="14">Potassium</text></g><g class="legend" transform="translate(216,0)"><rect width="18" height="18" style="fill: rgb(63, 82, 88); stroke: rgb(63, 82, 88);"></rect><text x="22" y="14">Magnesium</text></g><g class="legend" transform="translate(216,22)"><rect width="18" height="18" style="fill: rgb(79, 47, 27); stroke: rgb(79, 47, 27);"></rect><text x="22" y="14">Iron</text></g></g></svg>

[More mermaid chart examples](https://mermaid-js.github.io/mermaid/#/examples)

### Preview snapshot
![](preview_snapshot.png)

### Cheat Sheet snapshot
![](cheatsheet_snapshot.png)
## License
Apache 2.0

