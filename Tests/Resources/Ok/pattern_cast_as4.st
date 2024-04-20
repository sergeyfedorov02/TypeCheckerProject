language core;

extend with #variants, #nullary-variant-labels, #records, #type-cast-patterns, #structural-patterns, #structural-subtyping;

fn f(x : { status : <| ok, error |> }) -> <| ok : Nat, error : Nat, invalid |> {
  return match x {
    { status = <| ok |>, result = n }
    cast as { status : <| ok |>, result : Nat }
    => <| ok = n |>
  | { status = <| error |>, error_code = n }
    cast as { status : <| error |>, error_code : Nat }
    => <| error = n |>
  | _ => <| invalid |>
  }
}

fn main(n : Nat) -> Nat {
  return n
}
