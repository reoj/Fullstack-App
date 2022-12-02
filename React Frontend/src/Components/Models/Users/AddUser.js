import React, { useContext, useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import ModalContext from "../../../Context/UI/modal-context";
import { useDispatch } from "react-redux";
import { sendNewUser } from "../../../Context/Users/users-redux-actions";
import { checkNoNulls } from "../../Helpers/CheckNoNulls";
import CustomFormGroup from "../../UI/CustomFormGroup";

function AddUser(props) {
  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const namefieldRef = useRef();
  const clfieldRef = useRef();
  const emailfieldRef = useRef();
  const phonefieldRef = useRef();

  const dsp = useDispatch();

  function onCloseHandle(oldData) {
    modalDispatch({ type: "CLOSE" });
  }
  function onSaveHandle(oldData) {
    const emptyFields = checkNoNulls([
      namefieldRef,
      clfieldRef,
      emailfieldRef,
      phonefieldRef,
    ]);
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
        phone: phonefieldRef.current.value,
      })
    );
    onCloseHandle();
  }

  return (
    <Form>
      <Modal.Body>
        <CustomFormGroup
          controlId="form_Name"
          label="Name"
          placeholder="User Name"
          reference={namefieldRef}
        ></CustomFormGroup>
        <CustomFormGroup
          controlId="form_class"
          label="Class"
          placeholder="User Class"
          reference={clfieldRef}
        ></CustomFormGroup>
        <CustomFormGroup
          controlId="form_mail"
          label="E-mail"
          placeholder="user@email.com"
          reference={emailfieldRef}
        ></CustomFormGroup>
        <CustomFormGroup
          controlId="form_tel"
          label="Phone"
          placeholder="000-000-0000"
          reference={phonefieldRef}
        ></CustomFormGroup>
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
