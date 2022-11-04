import React, { useContext } from "react";
import ModalContext from "../../../Context/modal-context";
import ActionButtons from "../../UI/ActionButtons";
import DeleteItem from "./DeleteItem";
import EditItem from "./EditItem";

function ItemSingle(props) {
  const context = useContext(ModalContext);
  const mc = context.setter;

  function onEditHandler(params) {
    mc({
      onDisplay: true,
      title: "Editing Item",
      body: (
        <EditItem
          item={{ id: props.idn, desc: props.description, st:props.state, owner: props.owner}}
        />
      ),
    });
  }

  function onDeleteHandler(params) {
    mc({
      onDisplay: true,
      title: "Please confirm deletion of the following Item",
      body: (
        <DeleteItem
          item={{ id: props.idn, desc: props.description, st:props.state, owner: props.owner}}
        />
      ),
    });
  }
  return (
    <tr>
      <td>{props.idn}</td>
      <td>{props.description}</td>
      
      <td>{props.state}</td>
      <td>{props.owner}</td>
      <ActionButtons
        clickForEdit={onEditHandler}
        clickForDel={onDeleteHandler}
        itemID={props.idn}
      />
    </tr>
  );
}

export default ItemSingle;
