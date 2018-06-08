import React from 'react'



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
            edges.forEach(function (element) {
                console.log(element);
                //console.log(window.location.href + 'Draw/Create/?Color=' + 1 + '&Edges[0]=2');
                location.href = window.location.href + 'Draw/Create/?Color={' + props.color + '}&Edges[0]=2';
                //  location.href = window.location.href + 'Draw/Create/?Color=' + props.color + '&Edges[0]={1,1,2,3}';
            });
        }

        function draw(e) {

            if (e.buttons !== 1) return; // if mouse is pressed.....

            ctx.beginPath(); // begin the drawing path

            ctx.lineWidth = 10; // width of line
            ctx.lineCap = 'round'; // rounded end cap
            ctx.strokeStyle = props.color; // hex color of line

            ctx.moveTo(pos.x, pos.y); // from position
            const edge = {
                startX: pos.x,
                startY: pos.y,
                endX: 0,
                endY: 0
        }
            setPosition(e);
            ctx.lineTo(pos.x, pos.y); // to position

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