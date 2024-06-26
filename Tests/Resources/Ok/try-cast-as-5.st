language core;
extend with #try-cast-as, #top-type, #structural-subtyping;

fn main(x : Nat) -> Top {
  return
    try { x } cast as Nat {
      y => true
    } with { 0 }
}