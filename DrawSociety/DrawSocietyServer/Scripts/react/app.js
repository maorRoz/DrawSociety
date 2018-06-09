import React from 'react';
import ReactDOM from 'react-dom';
import Drawer from './draw'
import HistoryDrawer from './historyDrawer'


ReactDOM.render((
        <div>
    <HistoryDrawer/>,
    <Drawer color="#41a854"/></div>),
    document.getElementById('app3'))