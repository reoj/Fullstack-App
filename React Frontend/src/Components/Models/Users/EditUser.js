import React, { useContext, useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import ModalContext from "../../../Context/UI/modal-context";
import { useDispatch } from "react-redux";
import { sendEditUser } from "../../../Context/Users/users-redux-actions";
import { checkNoNulls } from "../../Helpers/CheckNoNulls";


function EditUser(props) {
  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const namefieldRef = useRef();
  const clfieldRef = useRef();
  const emailfieldRef = useRef();
  const phonefieldRef = useRef();

  const dsp = useDispatch();

  function onCloseHandle(oldData) {
    modalDispatch({type:"CLOSE"})
  }
  function onSaveHandle(oldData) {
    const emptyFields = checkNoNulls([namefieldRef, clfieldRef]);
    if (emptyFields.length !== 0) {
      emptyFields.forEach((f) => {
        f.current.className = "form-control bg-danger";
      });
      return;
    }
    dsp(
      sendEditUser({
        userId:props.item.id,
        name: namefieldRef.current.value,
        userType: clfieldRef.current.value,
        email: emailfieldRef.current.value,
        phone: phonefieldRef.current.value
      })
    );
    onCloseHandle(oldData);
  }
  function onInputClarity(event) {
    event.target.className = "form-control";
  }

  return (
    <Form>
      <Modal.Body>
      <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Name</Form.Label>
          <Form.Control
            type="text"
            placeholder="User Name"
            ref={namefieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.name}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Class</Form.Label>
          <Form.Control
            type="text"
            placeholder="User Class"
            ref={clfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.cl}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>E-mail</Form.Label>
          <Form.Control
            type="text"
            placeholder="user@email.com"
            ref={emailfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.email}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Phone</Form.Label>
          <Form.Control
            type="text"
            placeholder="000-000-0000"
            ref={phonefieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.phone}
          />
        </Form.Group>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="primary" onClick={onCloseHandle}>
          Close
        </Button>
        <Button variant="warning" onClick={onSaveHandle}>
          Save changes
        </Button>
      </Modal.Footer>
    </Form>
  );
}

export default EditUser;
