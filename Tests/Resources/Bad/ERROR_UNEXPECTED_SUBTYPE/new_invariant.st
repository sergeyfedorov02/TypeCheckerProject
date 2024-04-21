language core;

extend with #references,
            #natural-literals,
            #top-type,
            #let-bindings,
            #structural-subtyping;

fn main(n : Nat) -> &Top {
  return let x = new(12) in x
}