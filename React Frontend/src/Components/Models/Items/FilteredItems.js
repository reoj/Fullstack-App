import React, { Fragment } from "react";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom/cjs/react-router-dom.min";
import TableDisplayer from "../../UI/TableDisplayer";
import ItemSingle from "./ItemSingle";

function FilteredItems(props) {
  const fullList = useSelector((state) => state.root.item.value.list);
  const userOnDisplay = useParams();

  const itemsList = fullList.filter(i=> i.owner === +userOnDisplay.userId)

  const properties = ["ID", "Description", "State", "Owner ID"];

  return (
    <Fragment>
    <TableDisplayer
      modelType="Filtered Items"
      colList={properties}
    >
      {itemsList.map((i) => {
        return (
          <ItemSingle
            key={"Item" + i.id.toString()}
            idn={i.id}
            description={i.desc}
            state={i.state}
            owner={i.owner}
          />
        );
      })}
    </TableDisplayer>
  </Fragment>
  );
}

export default FilteredItems;
