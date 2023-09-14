## System Context
![A test image](Result/SystemContext.png =500x)

## Container Context
Coming soon

## Use structurizr to generate the diagrams
Start the structurizr lite docker container:
``` shell
docker pull structurizr/lite
docker run -it --rm -p 8080:8080 -v PATH:/usr/local/structurizr structurizr/lite
```
Change PATH to your workspace directory

Visit http://localhost:8080/workspace/diagrams to start the structurizr lite web application.

Use visual code to edit the workspace file.

## Reference
* [Structurizr Install Guide](https://docs.structurizr.com/lite/installation)