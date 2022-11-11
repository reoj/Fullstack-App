import React, { useContext, useRef} from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import ModalContext from "../../../Context/modal-context";
import { useDispatch } from "react-redux";
import { sendNewUser } from "../../../Context/users-redux-actions";

function AddUser(props) {
  const modalCtx = useContext(ModalContext)
  const mc = modalCtx.setter;

  const namefieldRef = useRef();
  const clfieldRef = useRef();
  const emailfieldRef = useRef();
  const phonefieldRef = useRef();

  const dsp = useDispatch()

  function onCloseHandle(oldData) {
    mc({
      ...oldData,
      onDisplay: false,
    });
  }
  function onSaveHandle(oldData) {
    const emptyFields = checkNoNulls([namefieldRef, clfieldRef, emailfieldRef, phonefieldRef]);
    if (emptyFields.length !== 0) {
      emptyFields.forEach((f) => {
        f.current.className = "form-control bg-danger";
      });
      return;
    }
    dsp(
      sendNewUser({
        name: namefieldRef.current.value,
        userType: clfieldRef.current.value,
        email: emailfieldRef.current.value,
        phone: phonefieldRef.current.value
      })
    );
    onCloseHandle(oldData);
  }
  function onInputClarity(event) {
    // event..className = "form-control"
    event.target.className = "form-control";
  }
  function checkNoNulls(arrayOfRefs) {
    const allEmptyFields = [];
    arrayOfRefs.forEach((r) => {
      if (r.current.value === "") {
        allEmptyFields.push(r);
      }
    });
    return allEmptyFields;
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
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Class</Form.Label>
          <Form.Control
            type="text"
            placeholder="User Class"
            ref={clfieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>E-mail</Form.Label>
          <Form.Control
            type="text"
            placeholder="user@email.com"
            ref={emailfieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Phone</Form.Label>
          <Form.Control
            type="text"
            placeholder="000-000-0000"
            ref={phonefieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onCloseHandle}>
          Close
        </Button>
        <Button variant="primary" onClick={onSaveHandle}>
          Save changes
        </Button>
      </Modal.Footer>
    </Form>

  );
}

export default AddUser;
