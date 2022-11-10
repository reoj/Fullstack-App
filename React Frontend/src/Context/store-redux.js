import thunk from 'redux-thunk'
import logger from 'redux-logger'
import rootReducer from './root-reducer'
import { configureStore } from '@reduxjs/toolkit'

export default configureStore({
    reducer: {
      root: rootReducer
    },
    middleware: [thunk, logger]
  })

