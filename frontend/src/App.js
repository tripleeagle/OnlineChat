import React from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import { Provider } from 'react-redux';

import Chat from './components/Chat';
import store from './store';

function App() {
  return (
    <Router>
      <Provider store={store}>
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
          <h2>Welcome to Chat</h2> <br/>
          <Switch>
              <Route path='/chatHome' component={ Chat } />
          </Switch>
        </div>
      </Provider>
    </Router>
  );
}

export default App;
