# Battlesnake structure created in C# and ASP.net

To start the snake, do the following:

```bash
git pull
docker build -t battlesnake .
docker stop battlesnake
docker run -dit --rm -p 5005:8080 battlesnake
```