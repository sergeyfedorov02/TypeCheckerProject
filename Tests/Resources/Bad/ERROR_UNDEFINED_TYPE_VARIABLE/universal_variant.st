language core;

extend with #universal-types, #variants, #type-cast;

generic fn do[T](x :<| a : C, b : Nat |>) -> T {
	return match x {
  	<| a = y |> => y
    | <| b = c |> => c cast as T
  }
}

fn main(x : Nat) -> Nat {
	return do[Nat](<| a = x |>)
}