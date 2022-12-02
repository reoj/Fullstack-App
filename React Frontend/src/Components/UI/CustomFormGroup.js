import React from "react";
import Form from "react-bootstrap/Form";

function CustomFormGroup(props) {
  const controlId = props.controlId;
  const label = props.label;
  const reference = props.reference;
  const placeholder = props.placeholder;
  const value = props.children

  function onInputClarity(event) {
    event.target.className = "form-control";
  }

  return (
    <Form.Group className="mb-3" controlId={controlId}>
      <Form.Label>{label}</Form.Label>
      <Form.Control
        type="text"
        placeholder={placeholder}
        ref={reference}
        onFocus={onInputClarity}
        defaultValue = {value}
      />
    </Form.Group>
  );
}

export default CustomFormGroup;
