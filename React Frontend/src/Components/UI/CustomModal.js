import React, { Fragment } from "react";

import Modal from "react-bootstrap/Modal";

function CustomModal(props) {
  const st = props.status;

  function onCloseHandle(oldData) {
    props.controller({
      ...oldData,
      onDisplay: false,
    });
  }
    
  return (
    <Modal show={st.onDisplay} onHide={onCloseHandle}>
      <Modal.Header closeButton>
        <Modal.Title>{st.title}</Modal.Title>
      </Modal.Header>

      <Fragment>
        {st.body}        
      </Fragment>
    </Modal>
  );
}

export default CustomModal;
