import React from 'react'
import axios from 'axios'

let colors = {
    green: "#41a854",
    red: "#d14829",
    yellow: "#f4dc41",
    black: "#141314",
    grey: "#9b989b",
    purple: "#910e8c",
    pink: "#f45fe0",
    blue: "#4286f4"

};

let choosenColorIndex = Math.floor(Math.random() * 8);
var choosenColor;
switch (choosenColorIndex) {
case 0:
    choosenColor = colors.green;
    break;
case 1:
    choosenColor = colors.red;
    break;
case 2:
    choosenColor = colors.yellow;
    break;
case 3:
    choosenColor = colors.black;
    break;
case 4:
    choosenColor = colors.grey;
    break;
case 5:
    choosenColor = colors.purple;
    break;
case 6:
    choosenColor = colors.pink;
    break;
case 7:
    choosenColor = colors.blue;

}


class Drawer extends React.Component {
    constructor(props) {
        super(props);
        const canvas = document.getElementById('draw');

        var ctx = canvas.getContext('2d');
        resize();
        function resize() {
            ctx.canvas.width = window.innerWidth;
            ctx.canvas.height = window.innerHeight;
        }

        document.addEventListener('resize', resize);
        document.addEventListener('mousemove', draw);
        document.addEventListener('mousedown', setPosition);
        document.addEventListener('mouseenter', setPosition);
        document.addEventListener('mouseup', printEdgesCount);

        var pos = { x: 0, y: 0 };

        function setPosition(e) {
            pos.x = e.clientX;
            pos.y = e.clientY;
        }

        var edges = [];
        function printEdgesCount() {
            let edgesString = [];
            for (let i = 0; i < edges.length; i++) {
                edgesString.push(JSON.stringify(edges[i]));
            }
            axios.post('/Draw/CreateShape',
                {
                    Color: choosenColor,
                    Board: document.getElementById("draw").getAttribute("board"),
                    Username: document.getElementById("draw").getAttribute("username"),
                    Edges: edgesString
        });

        }

        function draw(e) {

            if (e.buttons !== 1) return; 

            ctx.beginPath(); 

            ctx.lineWidth = 10; 
            ctx.lineCap = 'round';
            ctx.strokeStyle = choosenColor;

            ctx.moveTo(pos.x, pos.y); 
            const edge = {
                startX: pos.x,
                startY: pos.y,
                endX: 0,
                endY: 0
            }
            setPosition(e);
            ctx.lineTo(pos.x, pos.y); 
            edge.endX = pos.x;
            edge.endY = pos.y;

            edges.push(edge);


            ctx.stroke(); 

        } 
    }
    render() {
        return null;
    }
}

export default Drawer