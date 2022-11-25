import React, { Fragment, useContext } from "react";

import Modal from "react-bootstrap/Modal";
import ModalContext from "../../Context/UI/modal-context";

function CustomModal(props) {
  const contxt = useContext(ModalContext);
  const st = contxt.properties;
  const dspatch = contxt.setter;

  function onCloseHandle() {
    dspatch({type:"CLOSE"})
  }

  return (
    <Modal show={st.onDisplay} onHide={onCloseHandle}>
      <Modal.Header closeButton>
        <Modal.Title>{st.title}</Modal.Title>
      </Modal.Header>

      <Fragment>{st.body}</Fragment>
    </Modal>
  );
}

export default CustomModal;
