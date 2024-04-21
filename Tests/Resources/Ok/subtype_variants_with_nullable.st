language core;

extend with #variants,
            #natural-literals,
            #top-type,
            #bottom-type,
            #structural-subtyping,
						#nullary-variant-labels;

fn fail(n : Nat) -> <| failure, value : Nat |> {
	return <| failure |>
}

fn main(n : Nat) -> <| value : Nat, failure, value2 : Bool |> {
  return fail(n)
}