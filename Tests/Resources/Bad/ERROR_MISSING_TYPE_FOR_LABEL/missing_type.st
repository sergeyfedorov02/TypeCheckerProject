language core;

extend with #variants,
            #structural-subtyping,
			#nullary-variant-labels;

fn fail(n : Nat) -> <| failure, value : Nat |> {
	return <| failure |>
}

fn main(n : Nat) -> <| value : Nat, failure : Nat, value2 : Bool |> {
  return fail(n)
}