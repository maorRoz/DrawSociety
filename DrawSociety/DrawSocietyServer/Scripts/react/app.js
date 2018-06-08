import React from 'react';
import ReactDOM from 'react-dom';
import Shape from './shapes'


//const something = <Shape/>;
ReactDOM.render((
    <div>
        <Shape name="rectangle" />
        <Shape name="circle" />
        <Shape name="triangle" />
        <Shape name="line" />
        <Shape name="elipse" />
    </div>
),
    document.getElementById('root'))