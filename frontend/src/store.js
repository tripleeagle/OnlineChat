import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers';
import signalrMiddleware from './middlewares/singalrMiddleware'
const initialState = {};

const middleware = [thunk, signalrMiddleware];

const store = createStore(
  rootReducer,
  initialState,
  compose(
    applyMiddleware(...middleware)
  )
);

export default store;