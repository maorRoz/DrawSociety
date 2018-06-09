import React from 'react'
import axios from 'axios'


class HistoryDrawer extends React.Component {
    constructor(props) {
        super(props);
        const canvas = document.getElementById('draw');
        var ctx = canvas.getContext('2d');

        let shapesToDraw = [];
        axios.get('http://'+window.location.host+'/api/DrawApi/GetShapes?board=' + document.getElementById("draw").
                getAttribute("board"))
            .then(function (response) {
                shapesToDraw = response.data;
                shapesToDraw.forEach(function (shape) {
                    let edgesToDraw = [];
                    axios.get('http://' + window.location.host +'/api/DrawApi/GetShapesEdges?shapeId=' + shape.Id)
                        .then(function (response) {
                            edgesToDraw = response.data;
                            edgesToDraw.forEach(function (edge) {
                                draw(edge.StartX, edge.StartY, edge.EndX, edge.EndY, shape.Color);
                            });
                    });
                });

            });
        function draw(startX,startY,endX,endY,color) {
            ctx.beginPath(); 
            ctx.lineWidth = 10; 
            ctx.lineCap = 'round'; 
            ctx.strokeStyle = color; 

            ctx.moveTo(startX, startY); 
            ctx.lineTo(endX, endY); 
            ctx.stroke(); 

        } 
    }
    render() {
        return null;
    }
}

export default HistoryDrawer