import React from 'react'

const Triangle = () => (
    <svg height="210" width="400">
        <path d="M150 0 L75 200 L225 200 Z" />
    </svg>
)

const Circle = () => (
    <svg height="100" width="100">
        <circle cx="50" cy="50" r="40" stroke="black" strokeWidth="3" fill="red" />
        Sorry, your browser does not support inline SVG.
    </svg> 
)

const Rectangle = () => (
    <svg width="400" height="110">
        <rect width="300" height="100" fill="rgb(0,0,255)" strokeWidth="3" stroke="rgb(0,0,0)" />
    </svg>
)


const Elipse = () => (
    <svg height="140" width="500">
        <ellipse cx="200" cy="80" rx="100" ry="50" fill="yellow" stroke="purple" strokeWidth="2" />
    </svg>
)

const Line = () => (
    <svg height="210" width="500">
        <line x1="0" y1="0" x2="200" y2="200" stroke="rgb(255,0,0)" strokeWidth="2" />
    </svg>
)

class Shape extends React.Component {
    render() {
        switch(this.props.name)
        {
            case "circle":
                return <Circle/>;
            case "triangle":
                return <Triangle/>;
            case "elipse":
                return <Elipse/>;
            case "rectangle":
                return <Rectangle/>;
            case "line":
                return <Line/>;
            default:
                return <h1>woops, there is no shape here!</h1> ;
        }
    }
}

export default Shape