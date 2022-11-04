import { combineReducers } from 'redux'
import  itemSlice  from './items-redux-slice'
import	userSlice from './user-redux-slice'

const rootReducer = combineReducers({
    user: userSlice,
    item: itemSlice
})

export default rootReducer