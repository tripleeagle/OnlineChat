import React from 'react';
import logo from './logo.svg';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Home from './Home';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';

function App() {
  return (
    <Router>
      <div className="container">
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <Link to={'/'} className="navbar-brand">React CRUD Example</Link>
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav mr-auto">
              <li className="nav-item">
                <Link to={'/'} className="nav-link">Home</Link>
              </li>
              <li className="nav-item">
                <Link to={'/chatHome'} className="nav-link">Start Chat</Link>
              </li>
            </ul>
          </div>
        </nav> <br/>
        <h2>Welcome to React CRUD Tutorial</h2> <br/>
        <Switch>
            <Route path='/chatHome' component={ Home } />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
