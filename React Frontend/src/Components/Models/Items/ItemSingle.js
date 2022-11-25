import React, { useContext } from "react";
import ModalContext from "../../../Context/UI/modal-context";
import ActionButtons from "../../UI/ActionButtons";
import DeleteItem from "./DeleteItem";
import EditItem from "./EditItem";

function ItemSingle(props) {
  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const single = {
    id: props.idn,
    name: props.name,
    description: props.description,
    quantity: props.quantity,
    userId: props.owner,
  };

  function onEditHandler() {
    modalDispatch({
      type: "OPEN",
      payload: {
        title: "Editing Item",
        body: (
          <EditItem
            item={single}
          />
        ),
      },
    });
  }

  function onDeleteHandler(params) {
    modalDispatch({
      type: "OPEN",
      payload: {
        title: "Please confirm deletion of the following Item",
        body: (
          <DeleteItem
            item={single}
          />
        ),
      },
    });
  }
  return (
    <tr>
      <td>{props.idn}</td>
      <td>{props.name}</td>
      <td>{props.description}</td>
      <td>{props.quantity}</td>
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
