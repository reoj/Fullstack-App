// Auxiliary Function ToDo: Custom Hook
export function checkNoNulls(arrayOfRefs) {
    const allEmptyFields = [];
    arrayOfRefs.forEach((r) => {
      if (r.current.value === "") {
        allEmptyFields.push(r);
      }
    });
    return allEmptyFields;
  }