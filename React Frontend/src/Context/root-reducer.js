import { combineReducers } from 'redux'
import  itemSlice  from './items-redux-slice'
import	userSlice from './user-redux-slice'
import statusSlice from './fetching-state-redux-slice'

const rootReducer = combineReducers({
    user: userSlice,
    item: itemSlice,
    status: statusSlice,
})

export default rootReducer