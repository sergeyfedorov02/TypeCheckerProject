language core;

extend with #variants,
            #structural-subtyping,
						#nullary-variant-labels;

fn fail(n : Nat) -> <| failure : Nat |> {
	return <| failure = 0 |>
}

fn main(n : Nat) -> <| failure |> {
  return fail(n)
}