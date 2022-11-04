import React, { Fragment} from "react";
import { useSelector } from "react-redux";
import TableDisplayer from "../../UI/TableDisplayer";
import ItemSingle from "./ItemSingle";

function ItemsList(props) {
  const itemsList = useSelector((state) => state.root.item.value.list);

  const properties = ["ID", "Description", "State","Owner ID"];

  return (
    <Fragment>
    <TableDisplayer
      modelType="Items"
      colList={properties}
    >
      {itemsList.map((i) => {
        return (
          <ItemSingle
            key={"Item" + i.id.toString()}
            idn={i.id}
            description={i.desc}
            owner={i.owner}
            state={i.state}
          />
        );
      })}
    </TableDisplayer>
  </Fragment>
  );
}

export default ItemsList;
