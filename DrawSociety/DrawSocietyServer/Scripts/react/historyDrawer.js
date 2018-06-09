import React from 'react'
import axios from 'axios'


class HistoryDrawer extends React.Component {
    constructor(props) {
        super(props);
        const canvas = document.getElementById('draw');

        // get canvas 2D context and set it to the correct size
        var ctx = canvas.getContext('2d');
       /* resize();
        // resize canvas when window is resized
        function resize() {
            ctx.canvas.width = window.innerWidth;
            ctx.canvas.height = window.innerHeight;
        }*/

        let shapesToDraw = [];
        axios.get('api/DrawApi/GetShapes?board=' + document.getElementById("draw").
                getAttribute("board"))
            .then(function (response) {
                shapesToDraw = response.data;
                shapesToDraw.forEach(function (shape) {
                    let edgesToDraw = [];
                    axios.get('api/DrawApi/GetShapesEdges?shapeId=' + shape.Id)
                        .then(function (response) {
                            edgesToDraw = response.data;
                            edgesToDraw.forEach(function (edge) {
                                draw(edge.StartX, edge.StartY, edge.EndX, edge.EndY, shape.Color);
                            });
                    });
                });

            });
        function draw(startX,startY,endX,endY,color) {
            ctx.beginPath(); // begin the drawing path
            ctx.lineWidth = 10; // width of line
            ctx.lineCap = 'round'; // rounded end cap
            ctx.strokeStyle = color; // hex color of line

            ctx.moveTo(startX, startY); // from position
            ctx.lineTo(endX, endY); // to position
            ctx.stroke(); // draw it!

        } 
    }
    render() {
        return null;
    }
}

export default HistoryDrawer