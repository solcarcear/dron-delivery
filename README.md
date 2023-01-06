# Drone Delivery Service

### Solution
To find the solution to this problem, 
I used Dijkstra's algorithm, taking the drones as starting points and forming the value of the arcs by subtracting the capacity of the drones 
from the weight of the deliveries, 
thus making a graph for each drone, and allowing me to evaluate the best way for each of them with the algorithm mentioned above.


### Tools
- IDE: Microsoft Visual Studio Enterprise 2022 (64-bit) - Current Version 17.2.6
- Framework: net6.0 
- DATAFILE: "it is in drone-delivery project at Data/data.txt"