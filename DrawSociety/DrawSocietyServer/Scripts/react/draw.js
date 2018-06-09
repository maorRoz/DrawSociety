﻿import React from 'react'
import axios from 'axios'



class Drawer extends React.Component {
    constructor(props) {
        super(props);
        const canvas = document.getElementById('draw');

        // get canvas 2D context and set it to the correct size
        var ctx = canvas.getContext('2d');
        resize();
        // resize canvas when window is resized
        function resize() {
            ctx.canvas.width = window.innerWidth;
            ctx.canvas.height = window.innerHeight;
        }

        // add event listeners to specify when functions should be triggered
        document.addEventListener('resize', resize);
        document.addEventListener('mousemove', draw);
        document.addEventListener('mousedown', setPosition);
        document.addEventListener('mouseenter', setPosition);
        document.addEventListener('mouseup', printEdgesCount);

        // last known position
        var pos = { x: 0, y: 0 };

        // new position from mouse events
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
                    Color: props.color,
                    Board: document.getElementById("draw").getAttribute("board"),
                    Edges: edgesString
        });

        }

        function draw(e) {

            if (e.buttons !== 1) return; // if mouse is pressed.....

            ctx.beginPath(); // begin the drawing path

            ctx.lineWidth = 10; // width of line
            ctx.lineCap = 'round'; // rounded end cap
            ctx.strokeStyle = props.color; // hex color of line

            ctx.moveTo(pos.x, pos.y); // from position
            console.log("start x:" + pos.x);
            console.log("start y:" + pos.y);
            const edge = {
                startX: pos.x,
                startY: pos.y,
                endX: 0,
                endY: 0
            }
            setPosition(e);
            ctx.lineTo(pos.x, pos.y); // to position
            console.log("end x:" + pos.x);
            console.log("end y:" + pos.y);
            edge.endX = pos.x;
            edge.endY = pos.y;

            edges.push(edge);


            ctx.stroke(); // draw it!

        } 
    }
    render() {
        return null;
    }
}

export default Drawer