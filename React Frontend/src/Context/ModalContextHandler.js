import React, { useReducer } from "react";
import ModalContext from "./UI/modal-context";

function ModalContextHandler(props) {
   
      function reducer(state, action) {
        if (action.type === "CLOSE") {
            return {onDisplay: false}
        }
        if (action.type === "OPEN") {
            return{onDisplay:true, title: action.payload.title, body: action.payload.body}
        }
      }
      const [state, dispatch] = useReducer(reducer, {onDisplay: false})
  return (
    <ModalContext.Provider
      value={{ properties: state, setter: dispatch }}
    >
      {props.children}
    </ModalContext.Provider>
  );
}

export default ModalContextHandler;
